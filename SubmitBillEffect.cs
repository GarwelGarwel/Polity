namespace Polity
{
    /// <summary>
    /// Effect of submitting bill for vote in Parliament
    /// </summary>
    public class SubmitBillEffect : Effect
    {
        public Bill Bill { get; set; }

        public Parliament Parliament { get; set; }

        public override string Description => "Submit " + Bill.Name;

        public override void Run()
        {
            base.Run();
            if (Parliament.Vote(Bill))
                Bill.Enact();
        }

        public SubmitBillEffect() => Bill = new Bill();

        public SubmitBillEffect(Bill bill, Parliament parliament)
        {
            Bill = bill;
            Parliament = parliament;
        }
    }
}
