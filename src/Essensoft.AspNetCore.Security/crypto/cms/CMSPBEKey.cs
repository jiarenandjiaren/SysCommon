using System;

using Essensoft.AspNetCore.Security.Asn1.Pkcs;
using Essensoft.AspNetCore.Security.Asn1.X509;
using Essensoft.AspNetCore.Security.Crypto;
using Essensoft.AspNetCore.Security.Crypto.Parameters;
using Essensoft.AspNetCore.Security.Utilities;

//import javax.crypto.interfaces.PBEKey;

namespace Essensoft.AspNetCore.Security.Cms
{
	public abstract class CmsPbeKey
		// TODO Create an equivalent interface somewhere?
		//	: PBEKey
		: ICipherParameters
	{
		internal readonly char[]	password;
		internal readonly byte[]	salt;
		internal readonly int		iterationCount;

		[Obsolete("Use version taking 'char[]' instead")]
		public CmsPbeKey(
			string	password,
			byte[]	salt,
			int		iterationCount)
			: this(password.ToCharArray(), salt, iterationCount)
		{
		}

		[Obsolete("Use version taking 'char[]' instead")]
		public CmsPbeKey(
			string				password,
			AlgorithmIdentifier keyDerivationAlgorithm)
			: this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}
		
		public CmsPbeKey(
			char[]	password,
			byte[]	salt,
			int		iterationCount)
		{
			this.password = (char[])password.Clone();
			this.salt = Arrays.Clone(salt);
			this.iterationCount = iterationCount;
		}

		public CmsPbeKey(
			char[]				password,
			AlgorithmIdentifier keyDerivationAlgorithm)
		{
            if (!keyDerivationAlgorithm.Algorithm.Equals(PkcsObjectIdentifiers.IdPbkdf2))
				throw new ArgumentException("Unsupported key derivation algorithm: "
                    + keyDerivationAlgorithm.Algorithm);

			Pbkdf2Params kdfParams = Pbkdf2Params.GetInstance(
				keyDerivationAlgorithm.Parameters.ToAsn1Object());

			this.password = (char[])password.Clone();
			this.salt = kdfParams.GetSalt();
			this.iterationCount = kdfParams.IterationCount.IntValue;
		}

		~CmsPbeKey()
		{
			Array.Clear(this.password, 0, this.password.Length);
		}

		[Obsolete("Will be removed")]
		public string Password
		{
			get { return new string(password); }
		}

		public byte[] Salt
		{
			get { return Arrays.Clone(salt); }
		}

		[Obsolete("Use 'Salt' property instead")]
		public byte[] GetSalt()
		{
			return Salt;
		}

		public int IterationCount
		{
			get { return iterationCount; }
		}

		public string Algorithm
		{
			get { return "PKCS5S2"; }
		}

		public string Format
		{
			get { return "RAW"; }
		}

		public byte[] GetEncoded()
		{
			return null;
		}

		internal abstract KeyParameter GetEncoded(string algorithmOid);
	}
}
