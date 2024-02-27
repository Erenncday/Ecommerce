﻿using Microsoft.AspNetCore.Http;

namespace EcommerceAPI.Application.Services
{
	public interface IFileService
	{
		Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);
		Task<string> FileRenameAsync(string FileName);
		Task<bool> CopyFileAsync(string path, IFormFile file);

	}
}
