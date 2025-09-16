extends Control


@onready var error = get_node("Error")

func _ready() -> void:
	var logger = GameLogger.new()
	OS.add_logger(logger)
	logger.error_caught.connect(show_error, ConnectFlags.CONNECT_DEFERRED)
	foo()

func show_error(message: String) -> void:
	error.text = message
	error.visible = true

func foo() -> void:
	bar()

func bar() -> void:
	load("res://scenes/MainMenu.tscn").instantiate().call_deferred("show")
