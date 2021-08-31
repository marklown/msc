import csv
import glob

from PyQt5.QtSql import QSqlDatabase, QSqlQuery

con = QSqlDatabase.addDatabase("QSQLITE")
con.setDatabaseName("stars")
con.open()

q = QSqlQuery()
q.exec(
	"""
	CREATE TABLE stars (
		id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL,
		Ra REAL,
		Dec REAL,
		Mag REAL,
	)
	"""
)

print(con.tables())

with open('../data/gsc11.csv') as csvfile:
	rdr = csv.reader(csvfile, delimiter=',')
	for row in rdr:
		if row[0].strip() == '':
			continue
		if int(row[7]) == 0:  # only make stars if the object class is stellar
			stars.append(Star(row))

for filename in glob.iglob('../Data/GSC11/gsc/**/*.gsc', recursive=True):
	print("parsing " + filename)

	file = open(filename)
	raw = file.read()
	end = raw.rfind('END')
	data_only = raw[end+4:len(raw)-1]
	data_only = data_only.lstrip()

	lines = []
	line_length = 45
#	with open('test.csv', 'a') as f:
	for i in range(0, len(data_only), line_length):
		line = data_only[i:i+line_length]
		lines.append(line)
		gsc_id = line[0:5]
		RA = line[5:14]
		Dec = line[14:23]
		pos_err = line[23:28]
		mag = line[28:33]
		mag_err = line[33:37]
		mag_band = line[37:39]
		classification = line[39:40] 
		plate_id = line[40:44]
		multiple = line[44:45]
		insert_with_params = """INSERT INTO stars (Ra, Dec, Mag) VALUES (?, ?, ?)"""
		data = (RA, Dec, mag)
		q.exec(insert_with_params)
	#	f.write("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}\n".format(gsc_id, RA, Dec, pos_err, mag, mag_err, mag_band, classification, plate_id, multiple))
	break