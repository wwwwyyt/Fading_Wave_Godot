extends Control

@export_group("UI")
@export var face : TextureRect
@export var char_name : Label
@export var text : Label

@export_group("对话")
@export var main_dialogue : DialogueGroup

var dialogue_index := 0
var typing_tween : Tween

func display_next_dialogue():
	if dialogue_index >= len(main_dialogue.dialogue_list):
		visible = false
		return
		
	var dialogue := main_dialogue.dialogue_list[dialogue_index]
	
	if dialogue == null:
		dialogue_index += 1
		return
	
	if dialogue.face:
		face.texture = dialogue.face
	else:
		face.texture
	if dialogue.char_name:
		char_name.text = dialogue.char_name
	else:
		char_name.text = ""
	
	if typing_tween and typing_tween.is_running(): # 当上一次的Tween正在播放
		typing_tween.kill() # 结束上一次的Tween
		text.text = dialogue.text # 直接显示全部文本
		dialogue_index += 1
	else:
		typing_tween = get_tree().create_tween()
		text.text = ""
		for character in dialogue.text:
			typing_tween.tween_callback(append_character.bind(character)).set_delay(0.05) 
		typing_tween.tween_callback(func(): dialogue_index += 1).set_delay(0.05)
		

func append_character(character : String):
	text.text += character

func _ready():
	display_next_dialogue()

func _on_click(event):
	if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT\
	and event.pressed:
		display_next_dialogue()
