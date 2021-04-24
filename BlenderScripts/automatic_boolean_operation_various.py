import bpy
import os
import glob
import queue
import functools
import random
random.seed(0)

data_path = "PATH/TO/FBX"
save_dir = "PATH/TO/SAVE"
start = True

def boolean_operation():

    id = random.randint(1,2)
    if (id==1):
    	bpy.ops.object.booltron_destructive_union(
                                            'INVOKE_DEFAULT',
                                            solver='EXACT',
                                            threshold=1e-06,
                                            use_pos_offset=True,
                                            pos_offset=0.005,
                                            merge_distance=0.0002,
                                            cleanup=True,
                                            triangulate=True,
                                            keep_objects=False,
                                            first_run=False
                                            )
    else:
        id = random.randint(1,2)
        if(id==1):
            bpy.ops.object.booltron_destructive_difference(
                                            'INVOKE_DEFAULT',
                                            solver='EXACT',
                                            threshold=1e-06,
                                            use_pos_offset=True,
                                            pos_offset=0.005,
                                            merge_distance=0.0002,
                                            cleanup=True,
                                            triangulate=True,
                                            keep_objects=False,
                                            first_run=False
                                            )
        else:
            bpy.ops.object.booltron_destructive_intersect(
                                            'INVOKE_DEFAULT',
                                            solver='EXACT',
                                            threshold=1e-06,
                                            use_pos_offset=True,
                                            pos_offset=0.005,
                                            merge_distance=0.0002,
                                            cleanup=True,
                                            triangulate=True,
                                            keep_objects=False,
                                            first_run=False
                                            )
	
def load_model(file):
    global start
    while start == False : print("waiting...")
    bpy.ops.import_scene.fbx(filepath=file)
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
    

files = glob.glob(data_path+"/*.fbx")
print("Going to process " + str(len(files)) + " fractals")
count = 0
for i in range(len(files)):
    file = files[i]
    bpy.app.timers.register(functools.partial(load_model,file),first_interval=1)
    bpy.app.timers.register(prepare_model,first_interval=1)
    bpy.app.timers.register(boolean_operation,first_interval=1)
    save_file = save_dir + "/" + os.path.splitext(os.path.basename(file))[0] + ".obj"
    bpy.app.timers.register(functools.partial(save_model,save_file),first_interval=1)
    count += 1
