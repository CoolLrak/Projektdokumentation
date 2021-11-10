public class RPCServer
{
	#region Fields

	private readonly Server _server;

	#endregion

	#region Constructors

	/// <summary>
	/// Creates the RPC-Server with the fewest necessary values
	/// </summary>
	/// <param name="host"></param>
	/// <param name="port"></param>
	/// <param name="EUROLabWorkflowPlannerService"></param>
	public RPCServer(string host, int port,
					 IEUROLabWorkflowPlannerService euroLabWorkflowPlannerService)
	{
		if (host == null)
			throw new NullReferenceException("host");
		if (port == 0)
			throw new NullReferenceException("Port");

		_server = new Server
		{
			Services =
			{
				EUROLabWorkflowPlannerService.BindService(
					new EUROLabWorkflowPlannerServiceServerImpl(euroLabWorkflowPlannerService))
			},
			Ports =
			{
				new ServerPort(host, port, ServerCredentials.Insecure)
			}
		};
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// tries to start the RPC-Server. Returns true if success
	/// </summary>
	/// <returns></returns>
	public bool StartServer()
	{
		try
		{
			_server.Start();
			return true;
		}
		catch (Exception exception)
		{
			return false;
		}
	}

	/// <summary>
	/// Shuts down the server
	/// </summary>
	public void StopServer()
	{
		_server.ShutdownAsync();
	}
}