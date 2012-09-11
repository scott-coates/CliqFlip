namespace CliqFlip.Domain.Entities.UserRoot
{
    public class ProfileImage
    {
        public string SquareImageUrl { get; private set; }
        public string LargeImageUrl { get; private set; }

        public ProfileImage(string squareImageUrl, string largeImageUrl)
        {
            SquareImageUrl = squareImageUrl;
            LargeImageUrl = largeImageUrl;
        }
    }
}