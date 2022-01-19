import socket
import sqlite3

from sqlite3 import Error


def createConnection(PATH):
	connection = None

	try:
		connection = sqlite3.connect(PATH)
		print("Connection to SQLite DB successful")
	except Error as e:
		print(f"The error '{e}' occurred")

	return connection

	 
def createUser(connection, data):
	sql = ''' INSERT INTO Users(data) VALUES(?) '''
	cur = connection.cursor()
	cur.execute(sql, data)
	connection.commit()


def updateUser(connection, data):
	sql = ''' UPDATE Users SET data = (?) WHERE id = 1 '''
	cur = connection.cursor()
	cur.execute(sql, data)
	connection.commit()


def checkToExists(connection):
	result = False
	sql = ''' SELECT id FROM Users WHERE id = 1 '''
	cur = connection.cursor()
	cur.execute(sql)
	data = cur.fetchall()
	if len(data) != 0: 
		result = True

	return result


def loadUser(connection):
	sql = ''' SELECT data FROM Users WHERE id = 1 '''
	cur = connection.cursor()
	cur.execute(sql)
	data = cur.fetchall()

	return data


def runServer():
	HOST = '127.0.0.1'  # Standard loopback interface address (localhost)
	PORT = 7777         # Port to listen on (non-privileged ports are > 1023)
	PATH = r"C:\Users\mmaks\Desktop\server_py\clickerGame"
	START = 0

	with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
		s.bind((HOST, PORT))
		s.listen()
		print(f"Server is ready on {HOST}:{PORT}...\nWaiting for users...")
		
		while True:
			conn, addr = s.accept()

			with conn:
				print('Connected by', addr)
				try:
					connection = createConnection(PATH)
					if(START == 0):
						START += 1
						bytes1 = bytes(str(loadUser(connection)[0]).strip('(').strip(',)').strip("b'").strip("'"), 'UTF-8')
						conn.sendall(bytes1)
						print("Data load successfuly!")
						conn.close()
					
						
					data = conn.recv(2048)
					DATA = []
					DATA.append(data)
					if checkToExists == False:
						createUser(connection, DATA)
					else:
						updateUser(connection, DATA)

					print(data)
				except:
					# break
					
					conn.close()
				
				
def main():
	runServer()


if __name__ == "__main__":
	main()