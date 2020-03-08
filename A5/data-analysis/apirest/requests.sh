# Lister les prédictions déjà réalisées
curl http://localhost:8000/prediction/subject_ppgs > all.json

# Faire une prédiction
curl -X POST -H "Content-Type: application/json" -d \
'{"chest_ACC_x": 0.742265, "chest_ACC_y": 0.049355, "chest_ACC_z": 0.580019, "chest_ECG": -0.01455, "chest_Resp": -1.245832, "wrist_ACC_x": -0.46875, "wrist_ACC_y": 0.537109, "wrist_ACC_z": 0.615234, "wrist_BVP": 37.290625, "weight": 57.0, "gender": 1, "age": 25, "height": 168.0, "skin": 4, "sport": 5, "wrist_EDA": 4.830127, "wrist_TEMP": 31.61, "activity": null}' \
http://localhost:8000/prediction/predict > prediction.json

# Demander une prédiction spécifique déjà réalisée
curl http://localhost:8000/prediction/subject_ppgs/1 > specific.json