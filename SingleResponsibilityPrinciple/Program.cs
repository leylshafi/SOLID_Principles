#region SRP_Before

class Employee
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Salary { get; set; }
    
    public void printTimesheetReport()
    {
        //do something ...
    }
}

#endregion



#region SRP_After
namespace After
{
    class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Salary { get; set; }

    }


    class TimeSheetReport
    {
        
        public void printTimesheetReport(Employee employee)
        {
            //do something ...
        }
    }
}




#endregion