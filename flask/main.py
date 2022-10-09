from flask import Flask, request, json, Response, jsonify
from flask_cors import CORS


app = Flask(__name__)
CORS(app)


q_VR= list()
q_SITE = list()


@app.route('/action', methods=['POST'])
def action():
    do_action = request.args['action']
    device = request.args['device']
    if device == 'VR':
        q_VR.append(do_action)
    else:
        q_SITE.append(do_action)
    print("VR: ", q_VR)
    print("SITE: ", q_SITE)
    return jsonify({'status': 'OK'})


@app.route('/get_action', methods=['GET', 'POST'])
def get_action():
    device = request.args['device']
    if device == 'VR' and q_VR:
        print('Отдал')
        return jsonify({'action': q_VR.pop(0)})
    elif device == 'SITE' and q_SITE:
        print('Отдал')
        return jsonify({'action': q_SITE.pop(0)})
    else:
        print("Empty queue")
        return jsonify({'action': 'Something went wrong'})


app.run(host="26.100.4.13")
