namespace ECommerce.Model
{

    public abstract class BaseModel
    {
        public int Id { get; set; }
        public BaseModel()
        {
            this.IsActive = true;
        }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        

    }
}
