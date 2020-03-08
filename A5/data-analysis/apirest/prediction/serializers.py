from rest_framework     import serializers
from prediction.models  import Subject_PPG

class Subject_PPG_Serializer(serializers.Serializer):
    chest_ACC_x     = serializers.FloatField()
    chest_ACC_y     = serializers.FloatField()
    chest_ACC_z     = serializers.FloatField()
    chest_ECG       = serializers.FloatField()
    chest_Resp      = serializers.FloatField()
    wrist_ACC_x     = serializers.FloatField()
    wrist_ACC_y     = serializers.FloatField()
    wrist_ACC_z     = serializers.FloatField()
    wrist_BVP       = serializers.FloatField()
    weight          = serializers.FloatField()
    gender          = serializers.IntegerField()
    age             = serializers.IntegerField()
    height          = serializers.FloatField()
    skin            = serializers.IntegerField()
    sport           = serializers.IntegerField()
    wrist_EDA       = serializers.FloatField()
    wrist_TEMP      = serializers.FloatField()
    activity        = serializers.FloatField(allow_null=True)

    def create(self, validated_data):
        """Create and return a new `Subject_PPG` instance, given the validated data."""
        return Subject_PPG.objects.create(**validated_data)


    def update(self, instance, validated_data):
        """Update and return an existing `Subject_PPG` instance, given the validated data."""
        instance.chest_ACC_x = validated_data.get('chest_ACC_x', instance.chest_ACC_x)
        instance.chest_ACC_y = validated_data.get('chest_ACC_y', instance.chest_ACC_y)
        instance.chest_ACC_z = validated_data.get('chest_ACC_z', instance.chest_ACC_z)
        instance.chest_ECG   = validated_data.get('chest_ECG', instance.chest_ECG)
        instance.chest_Resp  =  validated_data.get('chest_Resp', instance.chest_Resp)
        instance.wrist_ACC_x =  validated_data.get('wrist_ACC_x', instance.wrist_ACC_x)
        instance.wrist_ACC_y =  validated_data.get('wrist_ACC_y', instance.wrist_ACC_y)
        instance.wrist_ACC_z = validated_data.get('wrist_ACC_z', instance.wrist_ACC_z)
        instance.wrist_BVP   = validated_data.get('wrist_BVP', instance.wrist_BVP)
        instance.weight      = validated_data.get('weight', instance.weight)
        instance.gender      = validated_data.get('gender', instance.gender)
        instance.age         = validated_data.get('age', instance.age)
        instance.height      = validated_data.get('height', instance.height)
        instance.skin        = validated_data.get('skin', instance.skin)
        instance.sport       = validated_data.get('sport', instance.sport)
        instance.wrist_EDA   = validated_data.get('wrist_EDA', instance.wrist_EDA)
        instance.wrist_TEMP  = validated_data.get('wrist_TEMP', instance.wrist_TEMP)
        #instance.activity      = validated_data.get('activity', instance.activity)
        instance.save()
        return instance
