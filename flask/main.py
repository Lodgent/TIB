from flask import Flask, request, json, Response, jsonify, render_template
from flask_cors import CORS
import logging

app = Flask(__name__)
CORS(app)
logging.basicConfig(filename='logging.log', level=logging.DEBUG)


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
        return jsonify({'action': q_SITE.pop(0)})
    else:
        return jsonify({'action': 'Something went wrong'})


app.run(host="26.100.4.13")
