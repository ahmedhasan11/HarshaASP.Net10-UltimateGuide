using Microsoft.AspNetCore.Mvc;

namespace File_Result.Controller
{
    public class HomeController: ControllerBase
    {
        public VirtualFileResult FileDownload()
        {
			//return new VirtualFileResult("file path", "content-type");
			//return new VirtualFileResult("/Ahmed-Hasan-ELbadawy-FlowCV-Resume-20250126", "application/pdf");
			return File("/Ahmed-Hasan-ELbadawy-FlowCV-Resume-20250126", "application/pdf");
		}

		public PhysicalFileResult FileDownload2()
		{
			//return new VirtualFileResult("Complete file path", "content-type");
			//return new PhysicalFileResult(@"C:\Users\user\source\repos\.NET\Harsha-ASP.NET-Complete-Guide\Controller\File Result\File Result\Ahmed-Hasan-ELbadawy-FlowCV-Resume-20250126 - Copy.pdf", "application/pdf");
			return PhysicalFile(@"C:\Users\user\source\repos\.NET\Harsha-ASP.NET-Complete-Guide\Controller\File Result\File Result\Ahmed-Hasan-ELbadawy-FlowCV-Resume-20250126 - Copy.pdf", "application/pdf");
		}
		public FileContentResult FileDownload3()
		{
			//System.IO.File.ReadAllBytes("Complete file path");
			//ReadAllBytes method will read the file and automatically the file contenht
			//is returned as a byte array
			 byte[] bytes =	System.IO.File.ReadAllBytes("@\"C:\\Users\\user\\source\\repos\\.NET\\Harsha-ASP.NET-Complete-Guide\\Controller\\File Result\\File Result\\Ahmed-Hasan-ELbadawy-FlowCV-Resume-20250126 - Copy.pdf\", \"application/pdf\"");

			//return new FileContentResult(bytearray , "content-type");
			//return new FileContentResult(bytes, "application/pdf");
			return File(bytes, "application/pdf");
		}
	}
}
