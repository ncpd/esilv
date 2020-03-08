from . import views
from django.urls import path
from django.conf.urls import url

urlpatterns = [
    path('', views.index),
    path('subject_ppgs', views.subject_ppg_list, name='list_of_subject_ppgs'),
    path('subject_ppgs/<int:pk>', views.ppg_detail, name='detail'),
    path('predict', views.predict)
]
