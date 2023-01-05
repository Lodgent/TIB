from flask import Flask, request, json, Response, jsonify, render_template
from flask_cors import CORS
import logging
import sys

app = Flask(__name__)
CORS(app)
logging.basicConfig(filename='logging.log', level=logging.DEBUG)
sessions = {
    "AAAA_VR": [],
    "AAAA_SITE": []

}


@app.route('/', methods=['GET'])
def start():
    return render_template('index.html')


@app.route('/action', methods=['POST'])
def action():
    do_action = request.args['action']
    device = request.args['device']
    code = request.args['code']
    if device == 'VR':
        sessions[code + '_VR'].append(do_action)
    else:
        sessions[code + '_SITE'].append(do_action)
    return jsonify({'status': 'OK'})


@app.route('/get_action', methods=['GET', 'POST'])
def get_action():
    device = request.args['device']
    code = request.args['code']
    if device == 'VR' and code + '_VR' in sessions:
        if len(sessions[code + '_VR']):
            return jsonify({'action': sessions[code + '_VR'].pop(0)})
    elif device == 'SITE' and code + '_SITE' in sessions:
        if len(sessions[code + '_SITE']):
            return jsonify({'action': sessions[code + '_SITE'].pop(0)})
    return jsonify({'action': 'Something went wrong'})


@app.route('/create_session', methods=['GET', 'POST'])
def create_session():
    code = request.args['code']
    sessions[code + "_VR"] = []
    sessions[code + "_SITE"] = []
    return jsonify({'status': 'OK'})


@app.route('/find_session', methods=['GET', 'POST'])
def find_session():
    code = request.args['code']
    if code + "_VR" in sessions:
        return jsonify({'status': 'Session find!'})
    else:
        return jsonify({'status': "Something went wrong"})



app.run(host="26.100.4.13", debug=True)
