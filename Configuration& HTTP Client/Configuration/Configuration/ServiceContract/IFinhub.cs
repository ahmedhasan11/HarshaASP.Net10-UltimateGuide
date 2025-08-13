namespace Configuration.ServiceContract
{
	public interface IFinhub
	{
		Task<Dictionary<string, object>> GetData(string symbol);
	}
}
