namespace PortafolioAPI.Models
{
    public class ResponseWithMessage
    {
        public Object? Data { get; set; }
        public string Message { get; set; } = "";
        public bool Success { get; set; }
        public List<string>? Errors { get; set; } = null;

        public void AddError(string error)
        {
            if (string.IsNullOrWhiteSpace(error)) { return; }
            if (Errors == null)
            {
                Errors = new();
                Errors.Add(error);
            }
            else
            {
                Errors.Add(error);
            }

        }
        public bool ErrorsExist()
        {
            if (Errors != null)
            {
                if (Errors.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class ResponseWithMessage<T> 
    {
        public T? Data { get; set; }
        public string Message { get; set; } = "";
        public bool Success { get; set; }
        public List<string>? Errors { get; set; } = null;

        public void AddError(string error)
        {
            if (string.IsNullOrWhiteSpace(error)) { return; }
            if (Errors == null)
            {
                Errors = new();
                Errors.Add(error);
            }
            else
            {
                Errors.Add(error);
            }

        }
        public bool ErrorsExist()
        {
            if (Errors != null)
            {
                if (Errors.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
