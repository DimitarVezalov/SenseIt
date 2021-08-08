namespace SenseIt.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SenseIt.Data.Models;

    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> cart)
        {
            cart.HasMany(x => x.CartItems)
                .WithOne(y => y.Cart)
                .HasForeignKey(y => y.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
