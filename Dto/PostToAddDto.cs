namespace DotnetAPI.Dtos
{
    public partial class PostToAddDto
    {

        public PostToAddDto()
        {
            if (PostTitle == null)
            {
                PostTitle = " ";
            }
            if (PostContent == null)
            {
                PostContent = " ";
            }
        }



        public int PostId {get;set;}
        public string PostTitle {get;set;}
        public string PostContent {get;set;}
        
      
         
     
    }
}