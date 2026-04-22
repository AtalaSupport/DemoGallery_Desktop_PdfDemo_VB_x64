# PdfDemo
Demonstrates how to view and save PDF files with DotImage and DotImage 
PdfDecoder.  Rasterizes a small thumbnail of each page in the PDF 
asynchronously while loading the first page in the PDF.  Demonstrates use of the 
ThumbnailView control.

This demo shows some uesful concepts related to PDF using DotImage

One of the more interesting features it has is to use the ExtractImages feature 
of our Atalasoft.Imaging.Codec.Pdf.Document class (not to be confused with 
Atalasoft.PdfDoc.PdfDocument)

This is the VB.NET version. We have a [C# version](https://github.com/AtalaSupport/DemoGallery_Desktop_OcrDiagnosticDemo_CS_x64) available.


## Licensing
This application requires a license for DotImage Document Imaging as well as our PdfReader addon. You may also request a 30 day evaulation if youre evaluating if DotImage is right for you.


## SDK Dependencies
This app was built based on 2026.2.0.0. It targets .NET Framework 4.6.2 and was created in Visual Studio 2022. You must have our SDK installed (and licesed per above)

[Download DotImage](https://www.atalasoft.com/BeginDownload/DotImageDownloadPage)


### OmniPage
Our OmniPageEngine has additional OCR resource requirements not included in our main SDK download (they would nearly double the size of the SDK download so we make them available separately to those who wish to use OmniPageEngine). They cna be found here: [INFO: OmniPageEngine Overview](https://www.atalasoft.com/kb2/KB/50396/INFO-OmniPageEngine-Overview)


### Using NuGet for SDK Dependencies
We do publish our SDK components to NuGet. We have chosen to base the demo on local installed SDK because this leads to much smaller applications (NuGet packages add a lot of overhead due to the way they're packaged and deployed, and many of our demos -- including this one -- are often used to reproduce issues that need to be submitted to support. Apps that use NuGet are often significantly larger and run up against our maximum support case upload size)

Still, if you wish to use NuGet for the dependencies instead of relying on locally installed SDK, you can.

- Take note of each of the references we've included:
    - Atalasoft.DotImage.dll
     - Atalasoft.DotImage.Lib.dll
    - Atalasoft.DotImage.Pdf.dll
    - Atalasoft.DotImage.PdfDoc.Bridge.dll
    - Atalasoft.DotImage.PdfReader.dll
    - Atalasoft.DotImage.WinControls.dll
    - Atalasoft.PdfDoc.dll
    - Atalasoft.Shared.dll
- Remove those referneces
- Open the NuGet Package Manger from `Tools -> NuGet Package Manager -> Manage NuGet Packages for this Solution`
- Browse for and install  Atalasoft.DotImage.WinControls.x64 - It will pull in DotImage Document Imaging (the base SDK) and our windows controls and shared dll
- Browse for and install Atalasoft.Pdf.x64  to bring in the PdfEncoder
- Browse for and install Atalasoft.PdfReader.x64. (optional if you wish to have support for PDF files)


## Downloading source
The sources can be downloaded for [c#](https://github.com/AtalaSupport/DemoGallery_Desktop_PdfDemo_CS_x64/archive/refs/heads/main.zip) and [VB.NET](https://github.com/AtalaSupport/DemoGallery_Desktop_PdfDemo_VB_x64/archive/refs/heads/main.zip)


## Cloning
We recommend the following if you wish to donload/clone a copy

Example: git for windows
```bash
git clone https://github.com/AtalaSupport/DemoGallery_Desktop_PdfDemo_VB_x64.git PdfDemo
```

## Related documentation
In addition to this README, the Atalasoft documentation set includes the following:  
- [AtalaSupport Github](https://github.com/AtalaSupport/) For an extensive set of sample apps.  
- [Atalasoft's APIs & Developer Guides page](https://www.atalasoft.com/Support/APIs-Dev-Guides) for our Developers guide and API references.  
- [Atalasoft Support](http://www.atalasoft.com/support/) for our main support portal.
- [Atalasoft Knowledgebase](http://www.atalasoft.com/kb2) where you can find answers to common questions / issues.  


## Getting Help for Atalasoft products
Atalasoft regularly updates our support [Knowledgebase](http://www.atalasoft.com/kb2) with the latest information about our products. To access some resources, you must have a valid Support Agreement with an authorized Atalasoft Reseller/Partner or with Atalasoft directly. Use the tools that Atalasoft provides for researching and identifying issues. 

Customers with an active evaluation, or those with active support / maintenance may [create a support case](https://www.atalasoft.com/Support/my-portal/Cases/Create-Case) 24/7, or call in to support ([+1 949 236-6510](tel:19492366510) ) during our normal support hours (Monday - Friday 8:00am to 5:00PM Eastern (New York) time).  

Customers who are unable to create a case or call in may [email our Sales Team](email:sales@atalasoft.com).  

