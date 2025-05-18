# Scan and Notify

Project implements a security monitoring application designed to detect unauthorized processes running on a system using RBAC, digital signatures and certificate-based communication. 
The central component of the system is the Malware Scanning Tool (MST), which periodically activates and checks the list of running processes against a predefined whitelist configuration.
This whitelist defines which processes are allowed to run and under which users.
When an unauthorized process is detected, MST raises an alert to the Intrusion Detection System (IDS).
