namespace InternTask1.Services.Abstract
{
    public interface IConfiguration
    {
        void SetNestingDegree(byte degree);
        void SetCsvFileName(string name);
        void SetCsvFilePath(string path);
        void SetExpEmail(string mail);
    }
}
