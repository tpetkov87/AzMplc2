param($installPath, $toolsPath, $package, $project)

  # Need to load MSBuild assembly if it's not loaded yet.
  Add-Type -AssemblyName 'Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'

  # Grab the loaded MSBuild project for the project
  $msbuild = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($project.FullName) | Select-Object -First 1
  $msbuild.Xml.Imports | 
  Where-Object { $_.Project -eq 'Build\RazorGenerator.MsBuild\build\RazorGenerator.MsBuild.targets' -or $_.Project -eq 'Build\FeatherPrecompilation.targets'} | 
  ForEach-Object { $msbuild.Xml.RemoveChild($_) }

  # Add project references
  Write-Host "Adding project references..."
  $references = "System.Web.Extensions", "System.Web.ApplicationServices"
  try
  {
    foreach ($reference in $references)
    {
      # Check if reference doesn't already exist
      $existingReference = $msbuild.Items | Where-Object { $_.ItemType -eq "Reference" -and $_.EvaluatedInclude.Split(",")[0] -eq $reference } | Select-Object -First 1
      if (!$existingReference)
      {
        Write-Host ("Adding '{0}' to {1}" -f $reference, $project.Name)

        $msbuild.AddItem("Reference", $reference)

        Write-Host ("Successfully added '{0}' to {1}" -f $reference, $project.Name)
      }
      else 
      {
        Write-Host ("{0} already has a reference to '{1}'" -f $project.Name, $reference)
      }
    }
  }
  catch
  {
    Write-Host ("Could not add references {0} to {1}. Please add these references manually or contact Sitefinity support." -f [system.String]::Join(", ", $references), $project.Name)
    Write-Error $_.Exception.Message
  }

  # Remove Recaptha views from Bootstrap package
  ForEach-Object { try { $project.ProjectItems.Item('ResourcePackages').ProjectItems.Item('Bootstrap').ProjectItems.Item('MVC').ProjectItems.Item('Views').ProjectItems.Item('Recaptcha') } catch { $null } } |
  Where-Object {$_ -ne $null} | 
  ForEach-Object { $_.Remove() }
  
  # Save changes to project
  $project.Save()

  $fileInfo = new-object -typename System.IO.FileInfo -ArgumentList $project.FullName
  $projectDirectory = $fileInfo.DirectoryName

  # Make sure all Resource Packages have RazorGenerator directives
  $generatorDirectivesPath = "$projectDirectory\ResourcePackages\Bootstrap\razorgenerator.directives"
  if (Test-Path $generatorDirectivesPath) 
  {
    Get-ChildItem "$projectDirectory\ResourcePackages" -Directory -Exclude "Bootstrap" |
    Where-Object { $_.GetFiles("razorgenerator.directives").Count -eq 0 } |
    ForEach-Object { Copy-Item $generatorDirectivesPath $_.FullName }
  }

  # Prompt to remove Recaptcha template if exists since it isn't distributed with Feather anymore
  $recaptchaTemplatesPath = "$projectDirectory\ResourcePackages\Bootstrap\MVC\Views\Recaptcha"
  if (Test-Path $recaptchaTemplatesPath) 
  {
    Remove-Item $recaptchaTemplatesPath -Recurse -Confirm
  }

  # Append attributes to the AssemblyInfo 
  Write-Host "Appending ControllerContainerAttribute and ResourcePackageAttribute to the AssemblyInfo..."
  $assemblyInfoPath = Join-Path $projectDirectory "Properties\AssemblyInfo.cs"
  if (Test-Path $assemblyInfoPath)
  {
    # Append ControllerContainerAttribute to the AssemblyInfo
	  $attributeRegex = "\[\s*assembly\s*\:\s*(?:(?:(?:(?:(?:(?:(?:Telerik\.)?Sitefinity\.)?Frontend\.)?Mvc\.)?Infrastructure\.)?Controllers\.)?Attributes\.)?ControllerContainer(?:Attribute)?\s*\]"
    $controllerContainerAttributeExists = (Get-Content $assemblyInfoPath | Where-Object { $_ -match $attributeRegex } | Group-Object).Count -eq 1
    if (!$controllerContainerAttributeExists)
    {
      Add-Content $assemblyInfoPath "`r`n[assembly: Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes.ControllerContainer]"
      Write-Host "Finished appending ControllerContainerAttribute to the AssemblyInfo."
    }
    else
    {
      Write-Host "ControllerContainerAttribute is already in the AssemblyInfo. Nothing is appended."
    }

    # Append ResourcePackageAttribute to the AssemblyInfo
    $attributeRegex = "\[\s*assembly\s*\:\s*(?:(?:(?:(?:(?:(?:(?:Telerik\.)?Sitefinity\.)?Frontend\.)?Mvc\.)?Infrastructure\.)?Controllers\.)?Attributes\.)?ResourcePackage(?:Attribute)?\s*\]"
    $resourcePackageAttributeExists = (Get-Content $assemblyInfoPath | Where-Object { $_ -match $attributeRegex } | Group-Object).Count -eq 1
    if (!$resourcePackageAttributeExists)
    {
      Add-Content $assemblyInfoPath "`r`n[assembly: Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes.ResourcePackage]"
      Write-Host "Finished appending ResourcePackageAttribute to the AssemblyInfo."
    }
    else
    {
      Write-Host "ResourcePackageAttribute is already in the AssemblyInfo. Nothing is appended."
    }
  }
  else
  {
    Write-Host "AssemblyInfo not found."
  }
# SIG # Begin signature block
# MIIXoAYJKoZIhvcNAQcCoIIXkTCCF40CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUikBxE8mjAkQ5j5LI5lRcQaIj
# UaSgghLGMIID7jCCA1egAwIBAgIQfpPr+3zGTlnqS5p31Ab8OzANBgkqhkiG9w0B
# AQUFADCBizELMAkGA1UEBhMCWkExFTATBgNVBAgTDFdlc3Rlcm4gQ2FwZTEUMBIG
# A1UEBxMLRHVyYmFudmlsbGUxDzANBgNVBAoTBlRoYXd0ZTEdMBsGA1UECxMUVGhh
# d3RlIENlcnRpZmljYXRpb24xHzAdBgNVBAMTFlRoYXd0ZSBUaW1lc3RhbXBpbmcg
# Q0EwHhcNMTIxMjIxMDAwMDAwWhcNMjAxMjMwMjM1OTU5WjBeMQswCQYDVQQGEwJV
# UzEdMBsGA1UEChMUU3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5bWFu
# dGVjIFRpbWUgU3RhbXBpbmcgU2VydmljZXMgQ0EgLSBHMjCCASIwDQYJKoZIhvcN
# AQEBBQADggEPADCCAQoCggEBALGss0lUS5ccEgrYJXmRIlcqb9y4JsRDc2vCvy5Q
# WvsUwnaOQwElQ7Sh4kX06Ld7w3TMIte0lAAC903tv7S3RCRrzV9FO9FEzkMScxeC
# i2m0K8uZHqxyGyZNcR+xMd37UWECU6aq9UksBXhFpS+JzueZ5/6M4lc/PcaS3Er4
# ezPkeQr78HWIQZz/xQNRmarXbJ+TaYdlKYOFwmAUxMjJOxTawIHwHw103pIiq8r3
# +3R8J+b3Sht/p8OeLa6K6qbmqicWfWH3mHERvOJQoUvlXfrlDqcsn6plINPYlujI
# fKVOSET/GeJEB5IL12iEgF1qeGRFzWBGflTBE3zFefHJwXECAwEAAaOB+jCB9zAd
# BgNVHQ4EFgQUX5r1blzMzHSa1N197z/b7EyALt0wMgYIKwYBBQUHAQEEJjAkMCIG
# CCsGAQUFBzABhhZodHRwOi8vb2NzcC50aGF3dGUuY29tMBIGA1UdEwEB/wQIMAYB
# Af8CAQAwPwYDVR0fBDgwNjA0oDKgMIYuaHR0cDovL2NybC50aGF3dGUuY29tL1Ro
# YXd0ZVRpbWVzdGFtcGluZ0NBLmNybDATBgNVHSUEDDAKBggrBgEFBQcDCDAOBgNV
# HQ8BAf8EBAMCAQYwKAYDVR0RBCEwH6QdMBsxGTAXBgNVBAMTEFRpbWVTdGFtcC0y
# MDQ4LTEwDQYJKoZIhvcNAQEFBQADgYEAAwmbj3nvf1kwqu9otfrjCR27T4IGXTdf
# plKfFo3qHJIJRG71betYfDDo+WmNI3MLEm9Hqa45EfgqsZuwGsOO61mWAK3ODE2y
# 0DGmCFwqevzieh1XTKhlGOl5QGIllm7HxzdqgyEIjkHq3dlXPx13SYcqFgZepjhq
# IhKjURmDfrYwggSjMIIDi6ADAgECAhAOz/Q4yP6/NW4E2GqYGxpQMA0GCSqGSIb3
# DQEBBQUAMF4xCzAJBgNVBAYTAlVTMR0wGwYDVQQKExRTeW1hbnRlYyBDb3Jwb3Jh
# dGlvbjEwMC4GA1UEAxMnU3ltYW50ZWMgVGltZSBTdGFtcGluZyBTZXJ2aWNlcyBD
# QSAtIEcyMB4XDTEyMTAxODAwMDAwMFoXDTIwMTIyOTIzNTk1OVowYjELMAkGA1UE
# BhMCVVMxHTAbBgNVBAoTFFN5bWFudGVjIENvcnBvcmF0aW9uMTQwMgYDVQQDEytT
# eW1hbnRlYyBUaW1lIFN0YW1waW5nIFNlcnZpY2VzIFNpZ25lciAtIEc0MIIBIjAN
# BgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAomMLOUS4uyOnREm7Dv+h8GEKU5Ow
# mNutLA9KxW7/hjxTVQ8VzgQ/K/2plpbZvmF5C1vJTIZ25eBDSyKV7sIrQ8Gf2Gi0
# jkBP7oU4uRHFI/JkWPAVMm9OV6GuiKQC1yoezUvh3WPVF4kyW7BemVqonShQDhfu
# ltthO0VRHc8SVguSR/yrrvZmPUescHLnkudfzRC5xINklBm9JYDh6NIipdC6Anqh
# d5NbZcPuF3S8QYYq3AhMjJKMkS2ed0QfaNaodHfbDlsyi1aLM73ZY8hJnTrFxeoz
# C9Lxoxv0i77Zs1eLO94Ep3oisiSuLsdwxb5OgyYI+wu9qU+ZCOEQKHKqzQIDAQAB
# o4IBVzCCAVMwDAYDVR0TAQH/BAIwADAWBgNVHSUBAf8EDDAKBggrBgEFBQcDCDAO
# BgNVHQ8BAf8EBAMCB4AwcwYIKwYBBQUHAQEEZzBlMCoGCCsGAQUFBzABhh5odHRw
# Oi8vdHMtb2NzcC53cy5zeW1hbnRlYy5jb20wNwYIKwYBBQUHMAKGK2h0dHA6Ly90
# cy1haWEud3Muc3ltYW50ZWMuY29tL3Rzcy1jYS1nMi5jZXIwPAYDVR0fBDUwMzAx
# oC+gLYYraHR0cDovL3RzLWNybC53cy5zeW1hbnRlYy5jb20vdHNzLWNhLWcyLmNy
# bDAoBgNVHREEITAfpB0wGzEZMBcGA1UEAxMQVGltZVN0YW1wLTIwNDgtMjAdBgNV
# HQ4EFgQURsZpow5KFB7VTNpSYxc/Xja8DeYwHwYDVR0jBBgwFoAUX5r1blzMzHSa
# 1N197z/b7EyALt0wDQYJKoZIhvcNAQEFBQADggEBAHg7tJEqAEzwj2IwN3ijhCcH
# bxiy3iXcoNSUA6qGTiWfmkADHN3O43nLIWgG2rYytG2/9CwmYzPkSWRtDebDZw73
# BaQ1bHyJFsbpst+y6d0gxnEPzZV03LZc3r03H0N45ni1zSgEIKOq8UvEiCmRDoDR
# EfzdXHZuT14ORUZBbg2w6jiasTraCXEQ/Bx5tIB7rGn0/Zy2DBYr8X9bCT2bW+IW
# yhOBbQAuOA2oKY8s4bL0WqkBrxWcLC9JG9siu8P+eJRRw4axgohd8D20UaF5Mysu
# e7ncIAkTcetqGVvP6KUwVyyJST+5z3/Jvz4iaGNTmr1pdKzFHTx/kuDDvBzYBHUw
# ggTMMIIDtKADAgECAhAzx7q9dVrb3SCyJ6xL/rqFMA0GCSqGSIb3DQEBCwUAMH8x
# CzAJBgNVBAYTAlVTMR0wGwYDVQQKExRTeW1hbnRlYyBDb3Jwb3JhdGlvbjEfMB0G
# A1UECxMWU3ltYW50ZWMgVHJ1c3QgTmV0d29yazEwMC4GA1UEAxMnU3ltYW50ZWMg
# Q2xhc3MgMyBTSEEyNTYgQ29kZSBTaWduaW5nIENBMB4XDTE2MTIwNTAwMDAwMFoX
# DTE4MTIyMTIzNTk1OVowZDELMAkGA1UEBhMCQkcxDjAMBgNVBAgMBVNvZmlhMQ4w
# DAYDVQQHDAVTb2ZpYTETMBEGA1UECgwKVEVMRVJJSyBBRDELMAkGA1UECwwCSVQx
# EzARBgNVBAMMClRFTEVSSUsgQUQwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEK
# AoIBAQDUWlr3Il6cjcUCipysOdAbuQRAJRvpX1cU1Bf5e526re0Dk8EAT9H8HV15
# QpD5GtmwaDzW8MHEJSDRGxeOoZvqfbHBecOo0LbKhlThndSbz3KMWzSZj3SqxZf6
# W526BiKOz+doiRX7Ot0grg1UotisSXYrwQCXkFFQGPHsYgpOhb7CPfjwaxlvoXWR
# fDslXXK+xtGPBiIp757QR7G5HREKcdoJrqyqsBzssHxje0cpFXmyivQIclgmhEJQ
# yfTXLT2oxtu1wJG2u5GehBrSVb+uMhUPMOFK35sM4GL/mV6sbFqHUzfBx/bMeYcw
# kd2Qvy0QHYIj1e95JRsNfru9nGt3AgMBAAGjggFdMIIBWTAJBgNVHRMEAjAAMA4G
# A1UdDwEB/wQEAwIHgDArBgNVHR8EJDAiMCCgHqAchhpodHRwOi8vc3Yuc3ltY2Iu
# Y29tL3N2LmNybDBhBgNVHSAEWjBYMFYGBmeBDAEEATBMMCMGCCsGAQUFBwIBFhdo
# dHRwczovL2Quc3ltY2IuY29tL2NwczAlBggrBgEFBQcCAjAZDBdodHRwczovL2Qu
# c3ltY2IuY29tL3JwYTATBgNVHSUEDDAKBggrBgEFBQcDAzBXBggrBgEFBQcBAQRL
# MEkwHwYIKwYBBQUHMAGGE2h0dHA6Ly9zdi5zeW1jZC5jb20wJgYIKwYBBQUHMAKG
# Gmh0dHA6Ly9zdi5zeW1jYi5jb20vc3YuY3J0MB8GA1UdIwQYMBaAFJY7U/B5M5ev
# fYPvLivMyreGHnJmMB0GA1UdDgQWBBSyKcHh97Gn6HMRM1wTMVaTsCzRKTANBgkq
# hkiG9w0BAQsFAAOCAQEAYmJfOhPyFGHLo4JD738DMmcj6RHx8cMDKSsfnU4S+sZE
# fi14vQFIVDBeDU8AWxAva7oWYHugzDmsgiEXYUhoH8fwG/CQgLGRkf9Ssm9hT32+
# 2uyzETwQbvo025QxcDIVBdpK6jtqs8tABsUff1IWZbLjSsjyZBv7RTNqV14NDL8B
# 2YJel90Z3QvvcKi8yNAapbLXOiDdK/Ii0ui9AFn2fzzciPJoORdn++Zkn8pQUUbf
# KJaFag042YL2duJ7HQBM9w3dXeCO2MucZ+Oo/+dPg4VCr83+Rvgk/mHq3Jm3fmlQ
# 8vVG04m+TJ+t/fl3NFGFoKDsbZFV5AyUqZMakVoUFzCCBVkwggRBoAMCAQICED14
# 1/l2SWCyYX308B7KhiowDQYJKoZIhvcNAQELBQAwgcoxCzAJBgNVBAYTAlVTMRcw
# FQYDVQQKEw5WZXJpU2lnbiwgSW5jLjEfMB0GA1UECxMWVmVyaVNpZ24gVHJ1c3Qg
# TmV0d29yazE6MDgGA1UECxMxKGMpIDIwMDYgVmVyaVNpZ24sIEluYy4gLSBGb3Ig
# YXV0aG9yaXplZCB1c2Ugb25seTFFMEMGA1UEAxM8VmVyaVNpZ24gQ2xhc3MgMyBQ
# dWJsaWMgUHJpbWFyeSBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eSAtIEc1MB4XDTEz
# MTIxMDAwMDAwMFoXDTIzMTIwOTIzNTk1OVowfzELMAkGA1UEBhMCVVMxHTAbBgNV
# BAoTFFN5bWFudGVjIENvcnBvcmF0aW9uMR8wHQYDVQQLExZTeW1hbnRlYyBUcnVz
# dCBOZXR3b3JrMTAwLgYDVQQDEydTeW1hbnRlYyBDbGFzcyAzIFNIQTI1NiBDb2Rl
# IFNpZ25pbmcgQ0EwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCXgx4A
# Fq8ssdIIxNdok1FgHnH24ke021hNI2JqtL9aG1H3ow0Yd2i72DarLyFQ2p7z518n
# TgvCl8gJcJOp2lwNTqQNkaC07BTOkXJULs6j20TpUhs/QTzKSuSqwOg5q1PMIdDM
# z3+b5sLMWGqCFe49Ns8cxZcHJI7xe74xLT1u3LWZQp9LYZVfHHDuF33bi+VhiXjH
# aBuvEXgamK7EVUdT2bMy1qEORkDFl5KK0VOnmVuFNVfT6pNiYSAKxzB3JBFNYoO2
# untogjHuZcrf+dWNsjXcjCtvanJcYISc8gyUXsBWUgBIzNP4pX3eL9cT5DiohNVG
# uBOGwhud6lo43ZvbAgMBAAGjggGDMIIBfzAvBggrBgEFBQcBAQQjMCEwHwYIKwYB
# BQUHMAGGE2h0dHA6Ly9zMi5zeW1jYi5jb20wEgYDVR0TAQH/BAgwBgEB/wIBADBs
# BgNVHSAEZTBjMGEGC2CGSAGG+EUBBxcDMFIwJgYIKwYBBQUHAgEWGmh0dHA6Ly93
# d3cuc3ltYXV0aC5jb20vY3BzMCgGCCsGAQUFBwICMBwaGmh0dHA6Ly93d3cuc3lt
# YXV0aC5jb20vcnBhMDAGA1UdHwQpMCcwJaAjoCGGH2h0dHA6Ly9zMS5zeW1jYi5j
# b20vcGNhMy1nNS5jcmwwHQYDVR0lBBYwFAYIKwYBBQUHAwIGCCsGAQUFBwMDMA4G
# A1UdDwEB/wQEAwIBBjApBgNVHREEIjAgpB4wHDEaMBgGA1UEAxMRU3ltYW50ZWNQ
# S0ktMS01NjcwHQYDVR0OBBYEFJY7U/B5M5evfYPvLivMyreGHnJmMB8GA1UdIwQY
# MBaAFH/TZafC3ey78DAJ80M5+gKvMzEzMA0GCSqGSIb3DQEBCwUAA4IBAQAThRoe
# aak396C9pK9+HWFT/p2MXgymdR54FyPd/ewaA1U5+3GVx2Vap44w0kRaYdtwb9oh
# BcIuc7pJ8dGT/l3JzV4D4ImeP3Qe1/c4i6nWz7s1LzNYqJJW0chNO4LmeYQW/Ciw
# sUfzHaI+7ofZpn+kVqU/rYQuKd58vKiqoz0EAeq6k6IOUCIpF0yH5DoRX9akJYmb
# BWsvtMkBTCd7C6wZBSKgYBU/2sn7TUyP+3Jnd/0nlMe6NQ6ISf6N/SivShK9DbOX
# Bd5EDBX6NisD3MFQAfGhEV0U5eK9J0tUviuEXg+mw3QFCu+Xw4kisR93873NQ9Tx
# TKk/tYuEr2Ty0BQhMYIERDCCBEACAQEwgZMwfzELMAkGA1UEBhMCVVMxHTAbBgNV
# BAoTFFN5bWFudGVjIENvcnBvcmF0aW9uMR8wHQYDVQQLExZTeW1hbnRlYyBUcnVz
# dCBOZXR3b3JrMTAwLgYDVQQDEydTeW1hbnRlYyBDbGFzcyAzIFNIQTI1NiBDb2Rl
# IFNpZ25pbmcgQ0ECEDPHur11WtvdILInrEv+uoUwCQYFKw4DAhoFAKB4MBgGCisG
# AQQBgjcCAQwxCjAIoAKAAKECgAAwGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQw
# HAYKKwYBBAGCNwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFLFs
# BVn0OHjNVWDuUNt8QkH6NALTMA0GCSqGSIb3DQEBAQUABIIBALgYVp/Cm6S25PBb
# 8VPVXZP34fyKAROzCfOzkXwDdghluXG2lrSGKsizJQdtJJ3c1QJfIUit7Ioa8y6a
# 4o574ocU2zV+sS5OJ8CcWCF45RD5pg4ROXh3mLQrxlztt0Yfh/xGgWMu8zn3Lnzk
# nXfj0L8LhyR+qTFkzaJ88Cnb88gaHG+uH6SUHW1APUp9B7dmTuHG9S1ULmHwl/k0
# MfjGQ11/ases56Ofzn4tPO6inOLH+MwuZWuO5JekaS+9/0P35L+O6fe1ezUUAVw8
# xRu6D2cgZt+OcB53WUDtcVzhXij0qpAAijeacw9nth49Ve9ci0KTng/KkLKCnEeW
# tYmJrN2hggILMIICBwYJKoZIhvcNAQkGMYIB+DCCAfQCAQEwcjBeMQswCQYDVQQG
# EwJVUzEdMBsGA1UEChMUU3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5
# bWFudGVjIFRpbWUgU3RhbXBpbmcgU2VydmljZXMgQ0EgLSBHMgIQDs/0OMj+vzVu
# BNhqmBsaUDAJBgUrDgMCGgUAoF0wGAYJKoZIhvcNAQkDMQsGCSqGSIb3DQEHATAc
# BgkqhkiG9w0BCQUxDxcNMTgwMTExMDgyMjMzWjAjBgkqhkiG9w0BCQQxFgQUaN2e
# lVPE8inB9Xio/SZOsCA8b4owDQYJKoZIhvcNAQEBBQAEggEAkp6Oj5djVEdc7yLL
# hW1phEXP1HiF+sb5dupAOKaJLFeoec/qusXNdwM9qs3qjNhOZBLIpN7+W/GHUzpD
# 4nvllLVhYSWcX4nZM0TT0EyFyCvi9Q5z2aI/3xEJApqNL/f/+4zC7ORQ2i0pKxSi
# r9e947qCNJUfvwfJ2VXD7Ch+lPSUsFKxX5Sqaium16+6SNfUaIkru2iFMENVCQod
# U5hMqbDPZwY/7br1cMIoC1crFT5/5LTZw/FF+W/c3lzeJtyyCRPr8vYnecG9N7QR
# 1vL4McWUN8Z9plE6x2T2vF+3bXiaoTrv9rUv08GH2VTIqKa3c7cVnyYvTyQTmrG8
# IbNrIQ==
# SIG # End signature block
