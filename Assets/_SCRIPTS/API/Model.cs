
[System.Serializable]
public class LoginRequest
{
    public string email;
    public string password;
}

[System.Serializable]
public class Model
{
    public bool status;
    public Account data;
}
[System.Serializable]
public class Account
{
    public int id;
    public string email;
    public string password;
    public string newPassword;
    public string name;
    public int level;
    public int exp;
    public float posX;
    public float posY;
}