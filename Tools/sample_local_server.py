import http.server
import socketserver
import os
PORT = 8000
DIRECTORY = "serverData/StandaloneOSX"

os.chdir(os.path.dirname(os.path.dirname(__file__)))
print(os.getcwd())
class Handler(http.server.SimpleHTTPRequestHandler):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, directory=DIRECTORY, **kwargs)


with socketserver.TCPServer(("", PORT), Handler) as httpd:
    print("serving at port", PORT)
    httpd.serve_forever()