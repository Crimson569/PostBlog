namespace PostService.Domain.Entities;

public class BaseEntity<T> where T : BaseEntity<T>
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not BaseEntity<T> entity)
        {
            return false;
        }

        if (ReferenceEquals(this, entity))
        {
            return true;
        }
        
        return Id == entity.Id;
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}