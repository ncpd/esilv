from django.http import JsonResponse
from django.shortcuts import render

def index(request):
    return JsonResponse({
        'urls': [
            {
                'url': '/prediction/',
                'description': 'Liste les routes possibles de l\'API de prédiction',
                'methods': ['GET']
            }
        ]
    })