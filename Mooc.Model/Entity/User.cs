using Mooc.Model.Entity.Course;

namespace Mooc.Model.Entity
{
    /// <summary>
    /// Gender Enum
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Male gender
        /// </summary>
        Male = 1,
        
        /// <summary>
        /// Female gender
        /// </summary>
        Female = 2,
        
        /// <summary>
        /// Other gender
        /// </summary>
        Other = 3
    }

    /// <summary>
    /// User Entity
    /// </summary>
    public class User : BaseEntity
    {
		/// <summary>
		/// User Name
		/// </summary>
		public string UserName {  get; set; } = string.Empty;
		
		/// <summary>
		/// Email Address
		/// </summary>
		public string Email {  get; set; } = string.Empty;
		
		/// <summary>
		/// Password
		/// </summary>
		public string Password {  get; set; }
		
		/// <summary>
		/// First Name
		/// </summary>
		public string FirstName {  get; set; } = string.Empty;
		
		/// <summary>
		/// Last Name
		/// </summary>
		public string LastName {  get; set; } = string.Empty;
		
		/// <summary>
		/// Phone Number
		/// </summary>
		public string Phone {  get; set; }
		
		/// <summary>
		/// Address
		/// </summary>
		public string Address {  get; set; }
		
		/// <summary>
		/// Gender
		/// </summary>
		public Gender? Gender {  get; set; }
		
		/// <summary>
		/// Date of Birth
		/// </summary>
		public DateTime? Dob {  get; set; }
		
		/// <summary>
		/// Avatar URL
		/// </summary>
		public string Avatar {  get; set; }
		
		/// <summary>
		/// Biography
		/// </summary>
		public string Bio {  get; set; }
		
		/// <summary>
		/// Created At
		/// </summary>
		public DateTime CreatedAt {  get; set; } = DateTime.Now;
		
		/// <summary>
		/// Updated At
		/// </summary>
		public DateTime? UpdatedAt {  get; set; }

		// Navigation properties
		/// <summary>
		/// User Roles (many-to-many)
		/// </summary>
		public virtual ICollection<Role> Roles { get; set; }
		
		/// <summary>
		/// Teacher Profile (one-to-one)
		/// </summary>
		public virtual Teacher TeacherProfile { get; set; }
		
		/// <summary>
		/// Courses taught by this user (one-to-many)
		/// </summary>
		public virtual ICollection<Course.Course> CoursesAsTeacher { get; set; }
	}
}
