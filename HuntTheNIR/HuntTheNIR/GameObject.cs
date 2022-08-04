
namespace HuntTheNIR
{
    public abstract class GameObject
    {
        protected GameObject(Coordinates location, char avatar)
        {
            Location = location;
            Avatar = avatar;
        }
        public char Avatar { get; set; }
        public Coordinates Location { get; set; }
        public abstract void Speech();
    }
}
