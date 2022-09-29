namespace Sign_Up_Form.Models.Interfaces
{
    public interface IPostRepository
    {
         
        public string MakeDate();
        public List<CommentUser> getAllComments(int postid);
        public void incComment(int id, String email);
        public void decComment(int id);
        public (Message, Comment) AddComment(Message m, Comment c);
        public void CreatePost(Post p);
        public List<Post> getTenPosts();
        public List<Post> getAdventouroursPosts();
        public List<Post> getGamingPosts();
        public List<Post> getSportsPosts();
        public void AddLikeToDb(Like l);
        public void RemoveLikeFromDb(Like l);
        public bool IsLiked(UserPost a, int id);
        public void addIncToLike(Like l);
        public void addDecToLike(Like l);
        public bool DeleteComment(int currentUserId, int CommentId, int MessageId);
        public bool ModifyComment(string message, int messageId);
        public bool DeletePost(int PostId);
        public bool DeleteAllNotificationsForThatPost(int PostId);
        public bool DeleteAllLikesForThatPost(int PostId);
        public bool DeleteAllCommentsForThatPost(int PostId);

        public List<Post> SearchTwentyPosts(String phrase, bool General, bool Sports, bool Gaming, bool Hiking);



    }
}
