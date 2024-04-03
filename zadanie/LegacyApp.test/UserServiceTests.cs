namespace LegacyApp.test;

public class UserServiceTests
{
    [Fact]
    public void AddUser_Return_False_When_Email_Doesnt_Contain_Dot_And()
    {
        string name = "Bob";
        string surname = "Smith";
        DateTime birthDateTime = new DateTime(1990, 10, 19);

        int id = 3;
        string email = "bobsmith";
        var service = new UserService();

        bool result = service.AddUser(name, surname, email, birthDateTime, id);
        
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Return_False_When_FirstName_Empty()
    {
        string name = null;
        string surname = "Smith";
        DateTime birthDateTime = new DateTime(1990, 10, 19);

        int id = 3;
        string email = "smith@gmail.pl";
        var service = new UserService();

        bool result = service.AddUser(name, surname, email, birthDateTime, id);
        
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Return_False_When_LastName_Empty()
    {
        string name = "Bob";
        string surname = null;
        DateTime birthDateTime = new DateTime(1990, 10, 19);

        int id = 3;
        string email = "smith@gmail.pl";
        var service = new UserService();

        bool result = service.AddUser(name, surname, email, birthDateTime, id);
        
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Return_False_When_ClientAge_Lower_Than_TwentyOne()
    {
        string name = "Bob";
        string surname = "Smith";
        DateTime birthDateTime = new DateTime(2010, 10, 19);

        int id = 3;
        string email = "smith@gmail.pl";
        var service = new UserService();

        bool result = service.AddUser(name, surname, email, birthDateTime, id);
        
        Assert.False(result);
    }
    [Fact]
    public void AddUser_Return_True_When_Very_Important_Client()
    {
        string name = "Bob";
        string surname = "Malewski";
        DateTime birthDateTime = new DateTime(1999, 10, 19);

        int id = 2;
        string email = "malewski@gmail.pl";
        var service = new UserService();

        bool result = service.AddUser(name, surname, email, birthDateTime, id);
        
        Assert.True(result);
    }
    [Fact]
    public void AddUser_Return_True_When_Important_Client()
    {
        string name = "Bob";
        string surname = "Smith";
        DateTime birthDateTime = new DateTime(1999, 10, 19);

        int id = 3;
        string email = "smith@gmail.pl";
        var service = new UserService();

        bool result = service.AddUser(name, surname, email, birthDateTime, id);
        
        Assert.True(result);
    }
    [Fact]
    public void AddUser_Return_True_When_Client()
    {
        string name = "Bob";
        string surname = "Kwiatkowski";
        DateTime birthDateTime = new DateTime(1999, 10, 19);

        int id = 5;
        string email = "kwiatkowski@wp.pl";
        var service = new UserService();

        bool result = service.AddUser(name, surname, email, birthDateTime, id);
        
        Assert.True(result);
    }
    [Fact]
    public void AddUser_Return_False_When_CreditLimit_Lower_Than_FiveHundred()
    {
        string name = "Bob";
        string surname = "Kowalski";
        DateTime birthDateTime = new DateTime(1999, 10, 19);

        int id = 1;
        string email = "kowalski@wp.pl";
        var service = new UserService();

        bool result = service.AddUser(name, surname, email, birthDateTime, id);
        
        Assert.False(result);
    }
    [Fact]
    public void AddUser_Exception_Doesnt_Exist()
    {
        string name = "Bob";
        string surname = "Xinski";
        DateTime birthDateTime = new DateTime(1999, 10, 19);

        int id = 7;
        string email = "xinski@wp.pl";
        var service = new UserService();


        Assert.Throws<ArgumentException>(() =>
        {
            service.AddUser(name, surname, email, birthDateTime, id);
        });
    }
}