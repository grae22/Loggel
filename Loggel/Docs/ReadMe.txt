===============
 Loggel ReadMe
===============

Design notes
------------

x Components should not store states, e.g. the Wire doesn't store if it's live - it queries its
  input socket.

ToDo
----

x Figure out how to restrict class visibilities (currently all are public).