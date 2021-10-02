namespace Mojang.NET.Core
{
    public class ApiResponse<T>
    {
        private readonly T response;

        public ApiResponse()
        {
        }

        public T Response => response;

        public bool Successful => response != null;
    }
}