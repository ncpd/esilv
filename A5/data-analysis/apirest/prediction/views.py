import numpy as np
from django.http import HttpResponse, JsonResponse
from django.shortcuts import render
from django.views.decorators.csrf import csrf_exempt
from rest_framework.parsers import JSONParser
from rest_framework.renderers import JSONRenderer
from sklearn.externals import joblib

from prediction.models import Subject_PPG
from prediction.serializers import Subject_PPG_Serializer


def index(request):
    return JsonResponse({
        'urls': [
            {
                'url': '/prediction/subject_ppgs',
                'description': 'Liste les données PPG disponibles',
                'methods': ['GET', 'POST']
            },
            {
                'url': '/prediction/subject_ppgs/:id',
                'description': 'Liste les données PPG correspondantes à l\'id passé',
                'methods': ['GET', 'PUT', 'DELETE']
            },
            {
                'url': '/prediction/predict',
                'description': 'Prédit l\'activité à partir des données en entrée',
                'methods': ['POST']
            }
        ]
    })

@csrf_exempt
def predict(request):
    """
    Renvoie les données avec l'activité completée prédite
    (Attend une activity == null)
    """
    if request.method != 'POST':
        return JsonResponse({ 'message': "Afin de prédire l'activité vous devez envoyer vos données en utilisant la méthode POST" }, status=400)
    elif request.method == 'POST':
        data        = JSONParser().parse(request)
        serializer  = Subject_PPG_Serializer(data=data)
        if serializer.is_valid():
            data["activity"] = predict_activity(data)
            serializer = Subject_PPG_Serializer(data=data)
            if serializer.is_valid():
                serializer.save()
                return JsonResponse(serializer.data, status=201)
        return JsonResponse(serializer.errors, status=400)

def predict_activity(unscaled_data):
    colonnes        = ["chest_ACC_x","chest_ACC_y","chest_ACC_z","chest_ECG","chest_Resp",
                       "wrist_ACC_x","wrist_ACC_y","wrist_ACC_z","wrist_BVP","weight",
                       "gender","age","height","skin","sport","wrist_EDA","wrist_TEMP"]
    path_to_model   = "./ipynb/randomforest_model.pkl"
    # on remet les data dans l'ordre des colonnes de la base d'apprentissage
    unscaled_data   = [unscaled_data[colonne] for colonne in colonnes]
    unscaled_data = np.array(unscaled_data).reshape(1,-1)
    print(unscaled_data)
    # on load le modèle appris préalablement
    model = joblib.load(path_to_model)
    # on prédit
    activity = model.predict(unscaled_data)
    # on renvoie la prédiction
    return activity

@csrf_exempt
def subject_ppg_list(request):
    if request.method == 'GET':
        Subjects      = Subject_PPG.objects.all()
        serializer  = Subject_PPG_Serializer(Subjects, many=True)
        return JsonResponse(serializer.data, safe=False)

    elif request.method == 'POST':
        data        = JSONParser().parse(request)
        serializer  = Subject_PPG_Serializer(data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data  , status=201)
        return     JsonResponse(serializer.errors, status=400)

@csrf_exempt
def ppg_detail(request, pk):
    try:
        subject_ppg = Subject_PPG.objects.get(pk=pk)
    except Subject_PPG.DoesNotExist:
        return HttpResponse(status=404)
    if request.method == 'GET':
        serializer = Subject_PPG_Serializer(subject_ppg)
        return JsonResponse(serializer.data)
    elif request.method == 'PUT':
        data       = JSONParser().parse(request)
        serializer = Subject_PPG_Serializer(subject_ppg, data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data)
        return JsonResponse(serializer.errors, status=400)
    elif request.method == 'DELETE':
        subject_ppg.delete()
        return HttpResponse(status=204)
    return HttpResponse(status=204)
