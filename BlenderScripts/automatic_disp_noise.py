import bpy
import os
import glob
import queue
import functools
import random


data_path = "PATH/TO/FBX"
save_dir = "PATH/TO/SAVE"
start = True
random.seed(0)
textures = ['NOISE','MARBLE','DISTORTED_NOISE','WOOD','STUCCI']

def disp_operation():
    if(random.random()>=0.5):
        global textures
        ob = bpy.context.object
        mod = ob.modifiers.new('Displace',type='DISPLACE')
        mod.strength = random.randint(1,3)/10
        tex = bpy.data.textures.new('Tex',type=random.choice(textures))
        mod.texture = tex
        
def load_model(file):
    global start
    while start == False : print("waiting...")
    bpy.ops.import_scene.obj(filepath=file)
    start = False
    
def save_model(file):
    global start
    bpy.ops.export_scene.obj(filepath=file,use_materials=False)
    bpy.ops.object.delete()
    print("SAVEDto: " + file)
    start = True
    
def prepare_model():
    objects = bpy.context.scene.objects
    bpy.context.view_layer.objects.active = objects[-1]
    for i in range(len(objects)): objects[i].select_set(True)
    

files = glob.glob(data_path+"/*.obj")
print("Going to process " + str(len(files)) + " fractals")
count = 0
for i in range(len(files)):
    file = files[i]
    bpy.app.timers.register(functools.partial(load_model,file),first_interval=1)
    bpy.app.timers.register(prepare_model,first_interval=1)
    bpy.app.timers.register(disp_operation,first_interval=1)
    save_file = save_dir + "/" + os.path.splitext(os.path.basename(file))[0] + ".obj"
    bpy.app.timers.register(functools.partial(save_model,save_file),first_interval=1)
    count += 1