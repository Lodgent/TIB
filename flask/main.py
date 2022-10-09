from flask import Flask, request, json, Response
app = Flask(__name__)

q_VR= []
q_SITE = []


@app.route('/')
def hello():
    return "Hello World!"


@app.route('/action', methods=['POST'])
def action():
    action = request.args['action']
    device = request.args['device']
    if device == 'VR':
        q_VR.append(action)
    else:
        q_SITE.append(action)
    print(q_VR)
    print(q_SITE)
    return json.dumps({'status': 'OK'})


@app.route('/get_action', methods=['POST'])
def get_action():
    device = request.args['device']
    if device == 'VR' and q_VR:
        return json.dumps({'status': 'OK',
                           'action': q_VR.pop(0)})
    elif device == 'SITE' and q_SITE:
        return json.dumps({'status': 'OK',
                           'action': q_SITE.pop(0)})
    else:
        return Response(status=204)


app.run(host="26.100.4.13", debug=True)
