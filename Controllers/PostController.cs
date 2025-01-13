using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class PostController : ControllerBase
    {
        private readonly DataContextEF _entityFramework;

        Mapper _mapper;

        public PostController(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);

            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PostToAddDto, Post>();
                cfg.CreateMap<PostToEditDto, Post>();

            }));
        }

        [HttpGet("Posts")]
        public IEnumerable<Post> GetPosts()
        {
            IEnumerable<Post> users = _entityFramework.Posts.ToList<Post>();
            return users;
        }

        [HttpGet("PostSingle/{postId}")]
        public Post GetSinglePost(int postId)
        {
            Post? usersPost = _entityFramework.Posts.Where(u => u.PostId == postId).FirstOrDefault<Post>();
            if (usersPost != null)
            {
                return usersPost;
            }
            throw new Exception("Failed to Get User");
        }

        [HttpGet("PostsByUser/{userId}")]
        public IEnumerable<Post> GetAllUsersPosts(int userId)
        {
            IEnumerable<Post> usersPost = _entityFramework.Posts.Where(u => u.UserId == userId).ToList<Post>();
            return usersPost;
        }

        [HttpGet("AllMyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            int userId = Convert.ToInt32(this.User.FindFirst("userId")?.Value);

            IEnumerable<Post> usersPost = _entityFramework.Posts.Where(u => u.UserId == userId).ToList<Post>();
            return usersPost;
        }


        [HttpGet("PostsBySearch/{searchParam}")]
        public IEnumerable<Post> PostsBySearch(string searchParam)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("userId")?.Value);

            // Filter posts by UserId and search parameter (PostTitle or PostContent)
            IEnumerable<Post> usersPost = _entityFramework.Posts
                .Where(u => u.UserId == userId &&
                            (u.PostTitle.Contains(searchParam) ||
                             u.PostContent.Contains(searchParam)))
                .ToList();

            return usersPost;
        }









        [HttpPost("Post")]

        public IActionResult AddPost(PostToAddDto postToAdd)
        {
            Post userPost = new Post
            {
                UserId = int.TryParse(this.User.FindFirst("userId")?.Value, out int userId) ? userId : throw new Exception("Invalid userId"),
                PostTitle = postToAdd.PostTitle,
                PostContent = postToAdd.PostContent,
                PostCreated = DateTime.Now,
                PostUpdated = DateTime.Now,
            };

            _entityFramework.Add(userPost);

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Add Post");
        }


        [HttpPut("EditPost")]

        public IActionResult EditPost(PostToEditDto postToEdit)
        {
            int userId = Convert.ToInt32(this.User.FindFirst("userId").Value);

            Post? postDb = _entityFramework.Posts.Where(u => u.UserId == userId).FirstOrDefault<Post>();


            if (postDb != null)
            {
                postDb.PostTitle = postToEdit.PostTitle;
                postDb.PostContent = postToEdit.PostContent;
                postDb.PostUpdated = DateTime.Now;
                if (_entityFramework.SaveChanges() > 0)
                {
                    return Ok();
                }

                throw new Exception("Failed to Update Post");


            }

            throw new Exception("Failed to Get Post");
        }



        [HttpDelete("DeletePost/{postId}")]

        public IActionResult DeletePost(int postId)
        {
            Post? postDb = _entityFramework.Posts.Where(u => u.PostId == postId).FirstOrDefault<Post>();

            _entityFramework.Remove(postDb);

            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Delete Post");
        }





    }




}