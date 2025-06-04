# DailyScribe

A C# console app for managing text notes. It allows:

- Creating new notes in `.txt` files
- Storing a list of notes in `entries.log` file
- Loading and selecting notes to read
- Editing and deleting selected lines in notes
- Encrypting and decrypting files with AES
- Basic input validation
- Basic unit tests for file operations and encryption

## Technologies

- **C# (console application)**
- **System.Security.Cryptography** – AES file encryption and decryption
- **Unit tests** – `xUnit` 
- Saving and reading text files (`.txt`, `entries.log`)

Data is saved in local text files.
