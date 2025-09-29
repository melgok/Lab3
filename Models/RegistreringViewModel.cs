namespace SkolSystem.Models
{
    public class RegistreringViewModel
    {
        public RegistreringViewModel() { }
        public IEnumerable<RegistreringDetails> RegistreringDetails{ get; set; }
        public IEnumerable<ElevDetails> ElevDetails { get; set; }
    }
}