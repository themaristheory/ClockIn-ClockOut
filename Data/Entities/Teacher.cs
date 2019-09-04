namespace ClockIn_ClockOut.Data.Entities
{
    /// <summary>
    /// This is the Teacher entity. It represents a teacher in the database.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Teacher's full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Teacher's user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Teacher's password.
        /// </summary>
        public string PassWord { get; set; }
    }
}