def isSet(pin, digit, decimal):
	if decimal and pin == 3:
		return True;
	if digit == 0:
		return pin == 11 or pin == 7 or pin == 4 or pin == 2 or pin == 1 or pin == 10;
	elif digit == 1:
		return pin == 7 or pin == 4;
	elif digit == 2:
		return pin == 11 or pin == 7 or pin == 5 or pin == 1 or pin == 2;
	elif digit == 3:
		return pin == 11 or pin == 7 or pin == 5 or pin == 4 or pin == 2;
	elif digit == 4:
		return pin == 10 or pin == 5 or pin == 7 or pin == 4;
	elif digit == 5:
		return pin == 11 or pin == 10 or pin == 5 or pin == 4 or pin == 2;
	elif digit == 6:
		return pin == 11 or pin == 10 or pin == 1 or pin == 2 or pin == 4 or pin == 5;
	elif digit == 7:
		return pin == 11 or pin == 7 or pin == 4;
	elif digit == 8:
		return pin == 11 or pin == 7 or pin == 4 or pin == 2 or pin == 1 or pin == 10 or pin == 5;
	elif digit == 9:
		return pin == 11 or pin == 7 or pin == 4 or pin == 2 or pin == 5 or pin == 10;
	else:
		return False;
