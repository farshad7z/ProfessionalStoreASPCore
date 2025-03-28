using System.Linq.Expressions;

namespace EShop.Core.Interfaces.Repositories
{

    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// یک موجودیت را بر اساس شناسه (ID) آن بازیابی می‌کند.
        /// </summary>
        /// <param name="id">شناسه (ID) موجودیت مورد نظر.</param>
        /// <returns>اگر موجودیت پیدا شود، آن را برمی‌گرداند؛ در غیر این صورت، مقدار null بازمی‌گرداند.</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// تمام موجودیت‌ها را با گزینه فیلتر کردن و بارگذاری داده‌های مرتبط بازیابی می‌کند.
        /// </summary>
        /// <param name="filter">عبارت فیلتر (Expression) برای اعمال شرط بر روی داده‌ها. اگر null باشد، تمام داده‌ها بازیابی می‌شوند.</param>
        /// <param name="includeProperties">لیست ویژگی‌های مرتبط (Navigation Properties) که باید شامل شوند. این ویژگی‌ها به صورت رشته‌ای با کاما جدا شده‌اند (مثل "Property1.Property2").</param>
        /// <returns>لیستی از موجودیت‌هایی که با شرط فیلتر مطابقت دارند.</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null);

        /// <summary>
        /// دریافت تمامی موجودیت‌ها با امکان فیلتر و بارگذاری روابط مرتبط به صورت Type-Safe.
        /// </summary>
        /// <param name="filter">عبارت شرطی برای فیلتر داده‌ها (اختیاری).</param>
        /// <param name="include">تابعی برای بارگذاری روابط مرتبط با استفاده از Include و ThenInclude (اختیاری).</param>
        /// <returns>لیستی از موجودیت‌های مورد نظر.</returns>
        public Task<IEnumerable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>> include = null);

        /// <summary>
        /// تمام موجودیت‌ها را با گزینه فیلتر کردن و بارگذاری داده‌های مرتبط به صورت IQueryableبازیابی می‌کند.
        /// </summary>
        /// <param name="filter">عبارت فیلتر (Expression) برای اعمال شرط بر روی داده‌ها. اگر null باشد، تمام داده‌ها بازیابی می‌شوند.</param>
        /// <param name="includeProperties">لیست ویژگی‌های مرتبط (Navigation Properties) که باید شامل شوند. این ویژگی‌ها به صورت رشته‌ای با کاما جدا شده‌اند (مثل "Property1.Property2").</param
        Task<IQueryable<T>> GetAllAsQueryable(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        /// <summary>
        /// موجودیت‌هایی را که با شرط مشخص‌شده مطابقت دارند، پیدا می‌کند.
        /// </summary>
        /// <param name="predicate">شرطی که برای یافتن موجودیت‌ها استفاده می‌شود.</param>
        /// <returns>لیستی از موجودیت‌هایی که با شرط مطابقت دارند.</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// یک موجودیت منفرد را که با شرط مشخص‌شده مطابقت دارد، پیدا می‌کند.
        /// </summary>
        /// <param name="predicate">شرطی که برای یافتن موجودیت استفاده می‌شود.</param>
        /// <returns>اگر موجودیت پیدا شود، آن را برمی‌گرداند؛ در غیر این صورت، مقدار null بازمی‌گرداند.</returns>
        Task<T?> FindSingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// بررسی می‌کند که آیا موجودیتی وجود دارد که با شرط مشخص‌شده مطابقت دارد یا خیر.
        /// </summary>
        /// <param name="predicate">شرطی که برای بررسی وجود موجودیت استفاده می‌شود.</param>
        /// <returns>اگر موجودیتی با شرط مطابقت داشته باشد، مقدار true بازمی‌گرداند؛ در غیر این صورت، مقدار false بازمی‌گرداند.</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// یک موجودیت جدید را به مخزن اضافه می‌کند.
        /// </summary>
        /// <param name="entity">موجودیتی که باید اضافه شود.</param>
        /// <returns>یک وظیفه (Task) که نشان‌دهنده عملیات ناهمزمان است.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// مجموعه‌ای از موجودیت‌های جدید را به مخزن اضافه می‌کند.
        /// </summary>
        /// <param name="entities">مجموعه‌ای از موجودیت‌هایی که باید اضافه شوند.</param>
        /// <returns>یک وظیفه (Task) که نشان‌دهنده عملیات ناهمزمان است.</returns>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// یک موجودیت موجود را در مخزن به‌روزرسانی می‌کند.
        /// </summary>
        /// <param name="entity">موجودیتی که باید به‌روزرسانی شود.</param>
        void Update(T entity);

        /// <summary>
        /// یک موجودیت را بر اساس شناسه (ID) آن حذف می‌کند.
        /// </summary>
        /// <param name="id">شناسه (ID) موجودیتی که باید حذف شود.</param>
        /// <returns>اگر موجودیت با موفقیت حذف شود، مقدار true بازمی‌گرداند؛ در غیر این صورت، مقدار false بازمی‌گرداند.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// یک موجودیت مشخص را از مخزن حذف می‌کند.
        /// </summary>
        /// <param name="entity">موجودیتی که باید حذف شود.</param>
        /// <returns>اگر موجودیت با موفقیت حذف شود، مقدار true بازمی‌گرداند؛ در غیر این صورت، مقدار false بازمی‌گرداند.</returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// مجموعه‌ای از موجودیت‌ها را از مخزن حذف می‌کند.
        /// </summary>
        /// <param name="entities">مجموعه‌ای از موجودیت‌هایی که باید حذف شوند.</param>
        /// <returns>یک وظیفه (Task) که نشان‌دهنده عملیات ناهمزمان است.</returns>
        Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}
