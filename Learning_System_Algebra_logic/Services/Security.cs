using System.Linq;
using System.Security.Cryptography;

namespace Learning_System_Algebra_logic.Services
{
	internal class Security
	{
		//private void FirstHash(string userName, string userPassword, int numberOfIterations) {
		//	Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(userPassword, 8, numberOfIterations);    //Hash the password with a 8 byte salt
		//	byte[] hashedPassword = PBKDF2.GetBytes(20);    //Returns a 20 byte hash
		//	byte[] salt = PBKDF2.Salt;
		//	writeHashToDB(userName, hashedPassword, salt, numberOfIterations);
		//}

		//private bool CheckPassword(string userName, string userPassword, int numberOfIterations) {
		//	byte[] usersHash = getUserHashFromFile(userName);
		//	byte[] userSalt = getUserSaltFromFile(userName);
		//	Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(userPassword, userSalt, numberOfIterations);    //Hash the password with the users salt
		//	byte[] hashedPassword = PBKDF2.GetBytes(20);    //Returns a 20 byte hash            
		//	bool passwordsMach = usersHash.SequenceEqual(hashedPassword);    //Compares byte arrays
		//	return passwordsMach;
		//}
	}
}