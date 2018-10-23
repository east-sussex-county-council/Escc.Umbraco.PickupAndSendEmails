# Escc.Umbraco.PickupAndSendEmails

Azure doesn't support sending SMTP emails by default. One way around this is to use the standard .NET approach to sending emails, but configure them to go to a folder:

	<system.net>
	  <mailSettings>
	    <smtp deliveryMethod="specifiedPickupDirectory" from="do-not-reply@example.org">
	        <specifiedPickupDirectory pickupDirectoryLocation="D:\home\site\example-folder" />
	    </smtp>
	  </mailSettings>
	</system.net>

This application can then be scheduled to run frequently, pick up the emails from the folder and send them on.