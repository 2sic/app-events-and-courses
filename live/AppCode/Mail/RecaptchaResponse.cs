using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppCode.Mail
{
    // The response from the Recaptcha Endpoint
  public class RecaptchaResponse
  {
    [JsonPropertyName("success")]
    public bool Success
    {
      get => m_Success;
      set => m_Success = value;
    }
    private bool m_Success;

    [JsonPropertyName("error-codes")]
    public List<string> ErrorCodes
    {
      get => m_ErrorCodes;
      set => m_ErrorCodes = value;
    }
    private List<string> m_ErrorCodes;

    [JsonPropertyName("hostname")]
    public string Hostname
    {
      get => m_Hostname;
      set => m_Hostname = value;
    }
    private string m_Hostname;
  }  
}



