# Halo 5 Server Looking for Group

This project lets your Xbox act as a host for custom games in Halo 5.  
It uses SCP driver and the Windows 10 streaming feature to run custom games and 
sends out invites to players requesting to join by message with the command 'inv'.

# Run
1.  Need SCP Driver installed
2.  Need your login.live.com cookie to paste in the program to scrap your messages

# TODO
1.  The timer values are wrong.  It needs investigation to figure out good values.
2.  Work on adding top forge maps and game modes.
3.  Add a login method instead of pasting the cookie in.

#ROADMAP
1.  Add error detection by screen capturing the GPU front buffer and using image process or pixel detection to determine the state.

# How to get cookie with chrome
*** NEVER SHARE YOUR COOKIE WITH ANYONE, IT GIVES FULL ACCESS TO YOUR MICROSOFT ACCOUNT***

1.  Right click and inspect
2.  Go to the network tab
3.  Go to https://account.xbox.com/en-US/Messages/UserConversation
4.  Click the request 'UserConversation'
5.  Copy the Cookie value under request header (including the 'Cookie: ' part)
6.  Paste it into the console app
