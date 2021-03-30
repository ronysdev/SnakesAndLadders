namespace Model.Configuration
{
    public interface IConfigLoader
    {
        ConfigModel Load(string fileName);
    }
}
