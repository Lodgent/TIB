from flask import Flask, request, json, Response, jsonify, render_template
from flask_cors import CORS
import logging

app = Flask(__name__)
CORS(app)
logging.basicConfig(filename='logging.log', level=logging.DEBUG)

current_level = 1

q_VR= list()
q_SITE = list()


@app.route('/', methods=['GET'])
def start():
    return render_template('index.html')


@app.route('/action', methods=['POST'])
def action():
    do_action = request.args['action']
    device = request.args['device']
    if device == 'VR':
        q_VR.append(do_action)
    else:
        q_SITE.append(do_action)
    return jsonify({'status': 'OK'})


@app.route('/get_action', methods=['GET', 'POST'])
def get_action():
    device = request.args['device']
    if device == 'VR' and q_VR:
        return jsonify({'action': q_VR.pop(0)})
    elif device == 'SITE' and q_SITE:
        if q_SITE[0] in ['Level2']:
            current_level += 1
        return jsonify({'action': q_SITE.pop(0)})
    else:
        return jsonify({'action': 'Something went wrong'})

@app.route("/get_current_level", methods = ['GET', 'POST'])
def get_current_level():
    return jsonify({'level': current_level})


app.run(host="25.32.13.14")
