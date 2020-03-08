from django.db import models

class Subject_PPG(models.Model):
    chest_ACC_x     = models.FloatField()
    chest_ACC_y     = models.FloatField()
    chest_ACC_z     = models.FloatField()
    chest_ECG       = models.FloatField()
    chest_Resp      = models.FloatField()
    wrist_ACC_x     = models.FloatField()
    wrist_ACC_y     = models.FloatField()
    wrist_ACC_z     = models.FloatField()
    wrist_BVP       = models.FloatField()
    weight          = models.FloatField()
    gender          = models.IntegerField()
    age             = models.IntegerField()
    height          = models.FloatField()
    skin            = models.IntegerField()
    sport           = models.IntegerField()
    wrist_EDA       = models.FloatField()
    wrist_TEMP      = models.FloatField()
    # The dependent variable: y
    activity        = models.FloatField(null=True)
    # Creation date
    created         = models.DateTimeField(auto_now_add=True)
