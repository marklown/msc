from star import Star
from dso import Dso
from PyQt5.QtSql import QSqlDatabase, QSqlQuery
import csv
import os


class Loader:
  def __init__(self, path):
    self.path = path

  def loadStars(self):
    print("Opening gsc11.csv...")
    stars = []
    with open(self.path + '/gsc11.csv') as csvfile:
      rdr = csv.reader(csvfile, delimiter=',')
      for row in rdr:
        if row[0].strip() == '':
          continue
        if int(row[7]) == 0:  # only make stars if the object class is stellar
          stars.append(Star(row))
    return stars

  def loadDsos(self):
    print("Opening dso.csv...")
    dsos = {}
    with open(self.path + '/dso.csv') as csvfile:
      rdr = csv.reader(csvfile, delimiter=',')
      next(rdr) # skip the header row
      for row in rdr:
        dso = Dso(row)
        dsos[dso.Cat + ' ' + dso.ID] = dso
    return dsos

  def loadStarsToDb(self):
    con = QSqlDatabase.addDatabase("QSQLITE")
    con.setDatabaseName("data/stars.db")
    con.open()
    with open(self.path + 'gsc11.csv') as csvfile:
      print("Opening gsc11.csv")
      rdr = csv.reader(csvfile, delimiter=',')
      index = 0
      for row in rdr:
        print("Parsing row " + str(index) + ' of 25541952', end='\r')
        if row[0].strip() == '':
          continue
        if int(row[7]) == 0:  # only make stars if the object class is stellar
          star = Star(row)
        query = QSqlQuery()
        query.prepare("INSERT INTO stars (Ra, Dec, Mag) VALUES (:ra, :dec, :mag)")
        query.bindValue(":ra", star.RA)
        query.bindValue(":dec", star.Dec)
        query.bindValue(":mag", star.Mag)
        query.exec_()
        index += 1
    con.close()

  def loadDsosToDb(self):
    con = QSqlDatabase.addDatabase("QSQLITE")
    con.setDatabaseName("data/stars.db")
    con.open()
    with open(self.path + 'dso.csv') as csvfile:
      print("Opening dso.csv")
      rdr = csv.reader(csvfile, delimiter=',')
      index = 0
      next(rdr) # skip the header row
      for row in rdr:
        print("Parsing row " + str(index) + ' of 227285', end='\r')
        dso = Dso(row)
        query = QSqlQuery()
        query.prepare("INSERT INTO dsos (Ra, Dec, Mag, Name, Cat, CatID, Type, Const) VALUES (:ra, :dec, :mag, :name, :cat, :catID, :type, :const)")
        query.bindValue(":ra", dso.RA)
        query.bindValue(":dec", dso.Dec)
        query.bindValue(":mag", dso.Mag)
        query.bindValue(":name", dso.Name)
        query.bindValue(":cat", dso.Cat)
        query.bindValue(":catID", dso.ID)
        query.bindValue(":type", dso.Type)
        query.bindValue(":const", dso.Const)
        query.exec_()
        index += 1
    con.close()
    
  def updateDsos(self):
    con = QSqlDatabase.addDatabase("QSQLITE")
    con.setDatabaseName("data/stars.db")
    con.open()
    with open(self.path + 'dso.csv') as csvfile:
      print("Opening dso.csv")
      rdr = csv.reader(csvfile, delimiter=',')
      index = 0
      next(rdr) # skip the header row
      for row in rdr:
        print("Parsing row " + str(index) + ' of 227285', end='\r')
        query = QSqlQuery()
        query.prepare("UPDATE dsos SET r1 = :r1, r2 = :r2, angle = :angle WHERE CatId = :catId AND Cat = :cat")
        query.bindValue(":r1", row[9])
        query.bindValue(":r2", row[10])
        query.bindValue(":angle", row[11])
        query.bindValue(":catId", row[13])
        query.bindValue(":cat", row[14])
        query.exec_()
        index += 1
    con.close()


if __name__ == '__main__':
  print("loader main")
  loader = Loader(os.getcwd() + "/data/")
  print(os.getcwd() + "/data/")
  loader.updateDsos()