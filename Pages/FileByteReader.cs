namespace Pages.Pages
{
    public class FileByteReader
    {
        public byte[] ReadBytesFromFile(string filePath)
        {
            try
            {
                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read all bytes from the file
                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    // You now have the bytes of the file in the 'fileBytes' array
                    return fileBytes;
                }
                else
                {
                    // Handle the case where the file does not exist
                    Console.WriteLine("File not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions that may occur during file reading
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            // Return null if there was an issue reading the file
            return null;
        }
    }
}