namespace DeliveryCo.DTOs
{
    //padrao dos sistemas que eu trabalho também
    public class IdentityResultCustom
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
