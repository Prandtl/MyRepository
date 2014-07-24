__author__ = 'Vasiliy'
#!/usr/bin/python3

import sys
import re
import datetime

LOG_LINE = 	r'^(?P<IP>\d+\.\d+\.\d+\.\d+)\s+' + \
			r'-\s+-\s+' + \
			r'\[(?P<DATE>.*?)\]\s+' + \
			r'"(?P<REQUEST>(?P<METHOD>[A-Z]+)\s+(?P<PATH>[^ ]+)\s+(?P<PROTO>[^"]+))"\s+' + \
			r'(?P<CODE>\d+)\s+(?P<SIZE>\d+)\s+' + \
			r'"(?P<REFERER>[^"]*)"\s+' + \
			r'"(?P<AGENT>[^"]*)"\s*' + \
			r'(?P<REQUEST_TIME>\d+)?\s*$'


def choose_file():
	if len(sys.argv) > 1:
		return sys.argv[1]
	else:
		return "logs.txt"

def parse_line(line):
	print(line)
	return re.match(LOG_LINE, line).groupdict()


def read_data(file, callback):
	with open(file_name) as logs:
		for line in logs:
			callback(parse_line(line))


def handle_log(log):
	global logs
	print(log)
	return
	logs.append(log)

def most_popular_url(logs):
	pass

def most_active_ip(logs):
	pass

def most_popular_borwser(logs):
	pass

def errors(logs):
	pass

file_name = choose_file()
logs = []

read_data(file_name, handle_log)



'http://yadi.sk/d/qm8G35WT3GjGG'
