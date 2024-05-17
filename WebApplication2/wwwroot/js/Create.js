// Import required modules
const express = require('express');
const fs = require('fs');

// Create an Express application
const app = express();

app.get('/createNewDocument', (req, res) => {
  // Get the file type from the query parameters
  const fileType = req.query.fileType;

  // Define the content of the new file
  const content = "Hello, world!";

  // Define the path where the new file will be created
  const filePath = `./yourFolder/newDocument${fileType}`;

  // Write the content to a new file
  fs.writeFile(filePath, content, (err) => {
    if (err) {
      console.log(err);
      res.status(500).send('An error occurred while creating the file.');
    } else {
      res.send('New document created successfully!');
    }
  });
});

// Start the server
app.listen(3000, () => console.log('Server is running on port 3000'));
