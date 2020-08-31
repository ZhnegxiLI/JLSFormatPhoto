using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using JLSDataModel.Models.Product;

namespace FormatPhoto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Job format photo start...");

            ProcessDirectory("C:\\Users\\zli\\source\\repos\\FormatPhoto\\FormatPhoto\\input", "C:\\Users\\zli\\source\\repos\\FormatPhoto\\FormatPhoto\\output");

            Console.WriteLine("Copy finish");
        }


        

        static void ProcessDirectory(string inputDirectory, string outputDirectory)
        {
            try
            {
                var db = new DataContext();
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(inputDirectory);
                Console.WriteLine("Retrived files successfully");
                foreach (string fileNameWithPath in fileEntries)
                {
                    /* Step1: Get only the file name without path */
                    var fileName = Path.GetFileName(fileNameWithPath);
                    Console.WriteLine("",fileName);

                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                    /* Step2: Remove the paraentheses and the content between parentheses */
                    var fileNameWithoutParentheses = fileNameWithoutExtension.Replace("（", "(").Replace("）", ")");
                    fileNameWithoutParentheses = Regex.Replace(fileNameWithoutParentheses.Replace("（", "(").Replace("）", ")"), @"\([^\(]*\)", "");

                    /* Step3: Check the folder exists or not in the target filePath */

                    var outputFilePath = outputDirectory + "\\" + fileNameWithoutParentheses;
                    if (!Directory.Exists(outputFilePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(outputFilePath);
                    }

                    /* Step4: Insert the file into folder */
                    File.Copy(fileNameWithPath, Path.Combine(outputFilePath, Path.GetFileName(fileName)) , true);// true present overwrite if exists

                    /* Step5: insert data into sql server */
                    var referenceId = db.ReferenceItem.Where(p => p.Code == fileNameWithoutParentheses).Select(p => p.Id).FirstOrDefault();
                    var productId = db.Product.Where(p => p.ReferenceItemId == referenceId).Select(p => p.Id).FirstOrDefault();


                  
                    if (productId!=0)
                    {
                        var productPhotoPath = new ProductPhotoPath();
                        productPhotoPath.CreatedBy = -1;
                        productPhotoPath.CreatedOn = DateTime.Now;
                        productPhotoPath.ProductId = productId;
                        productPhotoPath.Path = Path.Combine("images", Path.GetFileName(fileName));

                        db.ProductPhotoPath.Add(productPhotoPath);
                    }
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
       
                
        }


    }
}
